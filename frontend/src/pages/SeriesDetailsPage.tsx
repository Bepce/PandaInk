import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { SeriesDetails } from "../types/SeriesDetails";
import "./SeriesDetailsPage.css";

function SeriesDetailsPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [series, setSeries] = useState<SeriesDetails | null>(null);
  const [loading, setLoading] = useState(true);
  const [inLibrary, setInLibrary] = useState(false);
  const [rating, setRating] = useState(0);
  const [reviewContent, setReviewContent] = useState("");

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
      alert("Review submitted!");
      setReviewContent(""); 
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
        // 1) Fetch series details
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

        // 2) Check if in library
        if (token) {
          const resLib = await fetch(`/api/library/${id}`, {
            headers: { Authorization: `Bearer ${token}` },
          });

          if (resLib.ok) {
            const raw = await resLib.json();
            // Supports boolean or wrapped object
            const parsed =
              typeof raw === "boolean"
                ? raw
                : Boolean(raw?.inLibrary ?? raw?.value);
            if (!cancelled) setInLibrary(parsed);
          }
        }
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
          {inLibrary && (
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
        </div>
      </div>

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
    </div>
  );
}

export default SeriesDetailsPage;
