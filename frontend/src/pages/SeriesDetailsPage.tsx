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
    async function fetchSeries() {
      if (!id) return;

      const token = localStorage.getItem("token");

      // Fetch series details
      const res = await fetch(`/api/series/${id}`, {
        headers: token ? { Authorization: `Bearer ${token}` } : {},
      });

      if (res.status === 401) {
        navigate("/login");
        return;
      }

      if (res.ok) {
        const data: SeriesDetails = await res.json();
        setSeries(data);
      } else {
        console.error("Failed to fetch series details");
        setLoading(false);
        return;
      }

      // Check if in library
      if (token) {
        const resLib = await fetch(`/api/series/${id}/in-library`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (resLib.ok) {
          const isInLib = await resLib.json();
          setInLibrary(isInLib);
        }
      }

      setLoading(false);
    }

    fetchSeries();
  }, [id, navigate]);

  const handleAddToLibrary = async () => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    const res = await fetch(`/api/library/add/${id}`, {
      method: "POST",
      headers: { Authorization: `Bearer ${token}` },
    });

    if (res.ok) {
      setInLibrary(true);
    } else {
      console.error("Failed to add to library");
    }
  };

  if (loading) return <p className="loading">Loading series details...</p>;
  if (!series) return <p className="not-found">Series not found</p>;

  return (
    <div className="series-details">
      <h1 className="series-title">{series.title}</h1>
      <div className="series-header">
        <img src={series.coverImage} alt={series.title} className="cover-img" />
        <div className="series-info">
          <h1>{series.title}</h1>
          <p><strong>Genre:</strong> {series.genre}</p>
          <p><strong>Release date:</strong> {series.releaseDate}</p>
          <p><strong>Author:</strong> {series.author}</p>
          <p><strong>Score:</strong> {series.score}</p>
          {inLibrary ? (null) : (
            <button className="add-btn" onClick={handleAddToLibrary}>
              Add to Library
            </button>
          )}
        </div>
      </div>
      
      {inLibrary ? (
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
      ) : null}
    </div>
  );
}

export default SeriesDetailsPage;
