import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { SeriesDetails } from "../types/SeriesDetails";
import { Review } from "../types/Review"; 
import "./SeriesDetailsPage.css";
import { log } from "console";

function SeriesDetailsPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [series, setSeries] = useState<SeriesDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const [inLibrary, setInLibrary] = useState(false);
  const [rating, setRating] = useState(0);
  const [reviewContent, setReviewContent] = useState("");
  const [reviews, setReviews] = useState<Review[]>([]);
  const [hasReview, setHasReview] = useState(false);
  const [editingReviewId, setEditingReviewId] = useState<string | null>(null);

  async function fetchReviews() {
    if (!id) return;
    try {
      const res = await fetch(`/api/review/${id}/all`);
      if (res.ok) {
        const data: Review[] = await res.json();
        setReviews(data);
      }
    } catch (err) {
      console.error(err);
    }
  }

  async function fetchSeriesDetails() {
  if (!id) return;
  const token = localStorage.getItem("token");

  const res = await fetch(`/api/series/${id}`, {
    headers: token ? { Authorization: `Bearer ${token}` } : {},
  });

  if (res.ok) {
    const data: SeriesDetails = await res.json();
    setSeries(data);
  }
}


  async function checkUserHasReview() {
    if (!id) return;
    const token = localStorage.getItem("token");
    if (!token) return;

    try {
      const res = await fetch(`/api/review/${id}/exists`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      if (res.ok) {
        const exists = await res.json();
        setHasReview(Boolean(exists));
      }
    } catch (err) {
      console.error(err);
    }
  }

  async function handleSubmitReview() {
  if (!series) return;

  const token = localStorage.getItem("token");
  if (!token) {
    navigate("/login");
    return;
  }

  const method = editingReviewId ? "PUT" : "POST";
  const url = editingReviewId ? `/api/review/${editingReviewId}` : `/api/review`;

  try {
    const body = editingReviewId
      ? {
          id: editingReviewId,
          content: reviewContent,
          rating: rating,
          createdAt: new Date().toISOString(),
          seriesId: series.id,
        }
      : {
          content: reviewContent,
          rating: rating,
          createdAt: new Date().toISOString(),
          seriesId: series.id,
        };

    const res = await fetch(url, {
      method,
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(body),
    });

    if (res.ok) {
      setReviewContent("");
      setRating(0);
      setEditingReviewId(null); // reset edit mode
      checkUserHasReview();
      fetchReviews();
      fetchSeriesDetails();
    } else if (res.status === 401) {
      navigate("/login");
    } else {
      console.error("Failed to submit review");
    }
  } catch (err) {
    console.error(err);
  }
}



  useEffect(() => {
    let cancelled = false;

    (async () => {
      if (!id) return;
      const token = localStorage.getItem("token");

      try {
        // Fetch series
        const res = await fetch(`/api/series/${id}`, {
          headers: token ? { Authorization: `Bearer ${token}` } : {},
        });

        if (!res) return;

        if (res.status === 401) {
          navigate("/login");
          return;
        }

        if (!res.ok) throw new Error("Failed to fetch series details");

        const data: SeriesDetails = await res.json();
        if (!cancelled) setSeries(data);

        if (token) {
          const resLib = await fetch(`/api/library/${id}`, {
            headers: { Authorization: `Bearer ${token}` },
          });

          if (resLib.ok) {
            const raw = await resLib.json();
            const parsed =
              typeof raw === "boolean"
                ? raw
                : Boolean(raw?.inLibrary ?? raw?.value);
            if (!cancelled) setInLibrary(parsed);
          }
        }


        fetchReviews();
        fetchSeriesDetails();

        if (token) checkUserHasReview();
      } catch (e) {
        console.error(e);
      } finally {
        if (!cancelled) setLoading(false);
      }
    })();

    return () => {
      cancelled = true;
    };
  }, [id, navigate]);

  async function handleAddToLibrary() {
    if (!id) return;
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    try {
      const res = await fetch(`/api/library/${id}`, {
        method: "POST",
        headers: {
          Authorization: `Bearer ${token}`,
          "Content-Type": "application/json",
        },
      });

      if (res.status === 401) {
        navigate("/login");
        return;
      }

      if (!res.ok) throw new Error("Failed to add to library");

      setInLibrary(true);

      const updated = await fetch(`/api/series/${id}`, {
        headers: { Authorization: `Bearer ${token}` },
      });
      if (updated.ok) {
        const data: SeriesDetails = await updated.json();
        setSeries(data);
      }
    } catch (err) {
      console.error(err);
    }
  }

function getCurrentUsername(): string | null {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    return payload.given_name || null; 
  } catch (err) {
    console.error("Invalid token", err);
    return null;
  }
}

const currentUserName = getCurrentUsername();


function handleEditReview(review: Review) {
  setEditingReviewId(review.id);
  setReviewContent(review.content);
  setRating(review.rating);
  window.scrollTo({ top: 0, behavior: "smooth" }); // scroll to form
}

async function handleDeleteReview(reviewId: string) {
  const token = localStorage.getItem("token");
  if (!token) {
    navigate("/login");
    return;
  }

  const confirmDelete = window.confirm(
    "Are you sure you want to delete this review? This action cannot be undone."
  );
  if (!confirmDelete) return;

  try {
    const res = await fetch(`/api/review/${reviewId}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (res.ok) {
      fetchReviews();
      checkUserHasReview();
    } else if (res.status === 401) {
      navigate("/login");
    } else {
      console.error("Failed to delete review");
    }
  } catch (err) {
    console.error(err);
  }
}


  if (loading) return <p className="loading">Loading series details...</p>;
  if (!series) return <p className="not-found">Series not found</p>;

  return (
    <div className="series-details">
      <h1 className="series-title">{series.title}</h1>

      <div className="series-header">
        <img src={series.coverImage} alt={series.title} className="cover-img" />
        <div className="series-info">
          <p><strong>Genre:</strong> {series.genre}</p>
          <p><strong>Release date:</strong> {series.releaseDate}</p>
          <p><strong>Author:</strong> {series.author}</p>
          <p><strong>Score:</strong> {series.score}</p>

          {!inLibrary && (
            <button className="add-btn" onClick={handleAddToLibrary}>
              Add to Library
            </button>
          )}
        </div>
      </div>

      {inLibrary && (!hasReview || editingReviewId) && (
  <div className="review-section card">
    <h3 className="review-title">
      {editingReviewId ? "Edit Your Review" : "Write a Review"}
    </h3>
    <textarea
      className="review-textarea"
      value={reviewContent}
      onChange={(e) => setReviewContent(e.target.value)}
      placeholder="Write your review..."
    />
    <div className="review-footer">
      <select
        className="review-rating"
        value={rating}
        onChange={(e) => setRating(Number(e.target.value))}
      >
        <option value={1}>⭐ 1</option>
        <option value={2}>⭐⭐ 2</option>
        <option value={3}>⭐⭐⭐ 3</option>
        <option value={4}>⭐⭐⭐⭐ 4</option>
        <option value={5}>⭐⭐⭐⭐⭐ 5</option>
      </select>
      <button className="review-submit" onClick={handleSubmitReview}>
        {editingReviewId ? "Update Review" : "Submit"}
      </button>
    </div>
  </div>
)}

      {inLibrary && (
        <div>
          <h2>Chapters</h2>
          <ul className="chapter-list">
            {series.chapters?.map((chapter) => (
              <li key={chapter.id} className="chapter-item">
                <Link to={`/chapter/${chapter.id}`}>{chapter.title}</Link>
              </li>
            ))}
          </ul>
        </div>
      )}
      <div className="reviews-list card" style={{ width: "100%", marginTop: "1rem", padding: "1rem" }}>
  <h3 style={{ marginBottom: "1rem" }}>User Reviews</h3>
  <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
  {reviews.map((review) => {
    const isOwner = review.createBy === currentUserName;


    return (
      <li
        key={review.id}
        style={{
          borderBottom: "1px solid #e5e5e5",
          paddingBottom: "0.75rem",
          marginBottom: "0.75rem",
        }}
      >
        <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "0.25rem" }}>
          <strong>{review.createBy || "Anonymous"}</strong>
          <span style={{ fontSize: "0.875rem", color: "#666" }}>
            {new Date(review.createdAt).toLocaleDateString()}
          </span>
        </div>
        <p style={{ margin: "0.25rem 0" }}>{review.content}</p>
        {review.rating > 0 && (
          <span style={{ fontSize: "0.9rem", color: "#f59e0b" }}>
            {"★".repeat(review.rating)} <span style={{ color: "#999" }}>({review.rating}/5)</span>
          </span>
        )}
        {isOwner && (
          <div style={{ display: "flex", gap: "0.5rem", marginTop: "0.5rem" }}>
            <button onClick={() => handleEditReview(review)}>
              Edit
            </button>
            <button
              onClick={() => handleDeleteReview(review.id)}
              style={{ color: "red" }}
            >
              Delete
            </button>
          </div>
        )}
      </li>
    );
  })}
</ul>
</div>
    </div>
  );
}

export default SeriesDetailsPage;
