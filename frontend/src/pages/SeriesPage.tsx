import { useEffect, useState } from "react";
import { Series } from '../types/Seires';
import './SeriesPage.css';


function SeriesPage() {
  const [series, setSeries] = useState<Series[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function fetchSeries() {
      try {
        const response = await fetch("/api/series");
        if (!response.ok) throw new Error("Failed to fetch series");
        const data: Series[] = await response.json();
        setSeries(data);
      } catch (err) {
        console.error(err);
      } finally {
        setLoading(false);
      }
    }

    fetchSeries();
  }, []);

  if (loading) return <p>Loading series...</p>;

  return (
    <div className="series">
      <h1>Series</h1>
      <div className="series-container">
        {series.map((s) => (
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

export default SeriesPage;