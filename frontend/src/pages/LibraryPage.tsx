import { useEffect, useState } from "react";
import { Series } from "../types/Seires";
import "./SeriesPage.css"; // reuse same styles

function LibraryPage() {
  const [library, setLibrary] = useState<Series[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchLibrary() {
      const token = localStorage.getItem("token");
      if (!token) {
        console.error("No token found — redirecting to login");
        window.location.href = "/login";
        return;
      }

      try {
        const response = await fetch("/api/library", {
          headers: { Authorization: `Bearer ${token}` },
        });
        if (!response.ok) throw new Error("Failed to fetch library");
        const data: Series[] = await response.json();
        setLibrary(data);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    }

    fetchLibrary();
  }, []);

  if (loading) return <p>Loading library...</p>;

  return (
    <div className="series">
      <h1 className="series-pagetitle">My Library</h1>
      <div className="series-container">
        {library.map((s) => (
          <div key={s.id} className="series-card">
            <img src={s.coverImage} alt={s.title} className="series-cover" />
            <h3>{s.title}</h3>
            <p>{s.author}</p>
            <p>{s.score}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default LibraryPage;
