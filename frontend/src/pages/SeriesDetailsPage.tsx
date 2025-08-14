import { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { SeriesDetails } from "../types/SeriesDetails";

function SeriesDetailsPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [series, setSeries] = useState<SeriesDetails | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchSeries() {
      if (!id) return;

      const token = localStorage.getItem("token");

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
      }

      setLoading(false);
    }

    fetchSeries();
  }, [id, navigate]);

  if (loading) return <p>Loading series details...</p>;
  if (!series) return <p>Series not found</p>;

  return (
    <div className="series-details">
      <h1>{series.title}</h1>
      <img src={series.coverImage} alt={series.title} style={{ maxWidth: "300px" }} />
      <p>Genre: {series.genre}</p>
      <p>Release date: {series.releaseDate}</p>
      <p>Author: {series.author}</p>
      <p>Score: {series.score}</p>
      <h2>Chapters</h2>
        <ul>
          {series.chapters?.map((chapter) => (
            <li key={chapter.id}>
              <Link to={`/chapter/${chapter.id}`}>Chapter {chapter.title}</Link>
            </li>
           ))}
        </ul>
    </div>
  );
}

export default SeriesDetailsPage;
