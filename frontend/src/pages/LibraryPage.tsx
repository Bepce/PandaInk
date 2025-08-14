import { useEffect, useState } from "react";
import { Series } from "../types/Seires";
import { useNavigate } from "react-router-dom";
import "./SeriesPage.css";

function LibraryPage() {
  const [series, setSeries] = useState<Series[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  async function fetchLibrary() {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    try {
      const res = await fetch("/api/library", {
        headers: { Authorization: `Bearer ${token}` },
      });

      if (res.status === 401) {
        navigate("/login");
        return;
      }

      if (!res.ok) throw new Error("Failed to fetch library");

      const data: Series[] = await res.json();
      setSeries(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  }

  async function handleRemove(id: string) {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    try {
      const res = await fetch(`/api/library/${id}`, {
        method: "DELETE",
        headers: { Authorization: `Bearer ${token}` },
      });

      if (!res.ok) throw new Error("Failed to remove from library");

      // Refresh library after removal
      fetchLibrary();
    } catch (err) {
      console.error(err);
    }
  }

  useEffect(() => {
    fetchLibrary();
  }, []);

  if (loading) return <p>Loading your library...</p>;

  return (
    <div className="series">
      <h1 className="series-pagetitle">My Library</h1>
      <div className="series-container">
        {series.map((s) => (
          <div key={s.id} className="series-card">
            <img
              src={s.coverImage}
              alt={s.title}
              className="series-cover"
              onClick={() => navigate(`/series/${s.id}`) }
              style={{cursor:"pointer"}}              
            />
            <h3>{s.title}</h3>
            <p>{s.author}</p>
            <p>{s.score}</p>
            <button
              className="remove-btn"
              onClick={() => handleRemove(s.id)}
            >
              Remove from Library
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default LibraryPage;
