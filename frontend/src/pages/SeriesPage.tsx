import { useEffect, useState } from "react";
import { Series } from '../types/Seires';
import './SeriesPage.css';
import { useNavigate } from "react-router-dom";


function SeriesPage() {
  const [series, setSeries] = useState<Series[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

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
  
  const handleClick = async (id: string) => {
    const token = localStorage.getItem("token");
    const res = await fetch(`/api/series/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });
    
    if (!res) return

    if (res.status === 401) {
      navigate("/login"); 
      return;
    }


    if (res.ok) {
      navigate(`/series/${id}`);
    }
  };

  if (loading) return <p>Loading series...</p>;

  return (
    <div className="series">
      <h1 className="series-pagetitle">Series</h1>
      <div className="series-container">
        {series.map((s) => (
          <div key={s.id} className="series-card" onClick={() => handleClick(s.id)} style={{cursor: 'pointer'}}>
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