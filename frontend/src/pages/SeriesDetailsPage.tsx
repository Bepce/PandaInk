import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { SeriesDetails } from "../types/SeriesDetails";
import { Review } from "../types/Review"; 
import "./SeriesDetailsPage.css";

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

  async function fetchReviews() {
    if (!id) return;
    try {
      const res = await fetch(`/api/reviews/${id}/all`);
      if (res.ok) {
        const data: Review[] = await res.json();
        setReviews(data);
      }
    } catch (err) {
      console.error(err);
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

    try {
      const res = await fetch(`/api/Review`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          content: reviewContent,
          rating: rating,
          createdAt: new Date().toISOString(),
          seriesId: series.id,
        }),
      });

      if (res.ok) {
        setReviewContent("");
        setRating(0);
        checkUserHasReview();
        fetchReviews();
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

        if (res.status === 401) {
          navigate("/login");
          return;
        }

        if (!res.ok) throw new Error("Failed to fetch series details");

        const data: SeriesDetails = await res.json();
        if (!cancelled) setSeries(data);

        // Check if in library
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

        // Fetch reviews
        fetchReviews();

        // Check if user already has a review
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

      {inLibrary && !hasReview && (
        <div className="review-section card">
          <h3 className="review-title">Write a Review</h3>
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
              <option value={0}>Not rated</option>
              <option value={1}>1</option>
              <option value={2}>2</option>
              <option value={3}>3</option>
              <option value={4}>4</option>
              <option value={5}>5</option>
            </select>
            <button className="review-submit" onClick={handleSubmitReview}>
              Submit
            </button>
          </div>
        </div>
      )}

      {/* Existing chapters */}
      {inLibrary && (
        <div>
          <h2>Chapters</h2>
          <ul className="chapter-list">
            {series.chapters?.map((chapter) => (
              <li key={chapter.id} className="chapter-item">
                <Link to={`/chapter/${chapter.id}`}>Chapter {chapter.title}</Link>
              </li>
            ))}
          </ul>
        </div>
      )}

      {/* Reviews card */}
      <div className="reviews-card card">
        <h3>Reviews</h3>
        {reviews.length === 0 && <p>No reviews yet.</p>}
        <ul className="review-list">
          {reviews.map((rev) => (
            <li key={rev.id} className="review-item">
              <p>{rev.userName}</p>
              <p><strong>Rating:</strong> {rev.rating || "Not rated"}</p>
              <p>{rev.content}</p>              
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
}

export default SeriesDetailsPage;
