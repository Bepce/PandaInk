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

      // Optimistic update
      setInLibrary(true);

      // Optional: refresh series after adding
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
