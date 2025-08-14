import { useEffect, useState } from "react";
import { Series } from '../types/Seires';
import './SeriesPage.css';
import { useNavigate } from "react-router-dom";

function SeriesPage() {
  const [series, setSeries] = useState<Series[]>([]);
  const [loading, setLoading] = useState(true);
  const [titleFilter, setTitleFilter] = useState("");
  const [sortBy, setSortBy] = useState("releaseDate");
  const [isDescending, setIsDescending] = useState(true);

  const navigate = useNavigate();

  const fetchSeries = async () => {
    setLoading(true);
    try {
      const query = new URLSearchParams();
      if (titleFilter) query.append("title", titleFilter);
      query.append("sortBy", sortBy);
      query.append("isDescending", isDescending.toString());

      const response = await fetch(`/api/series?${query.toString()}`);
      if (!response.ok) throw new Error("Failed to fetch series");
      const data: Series[] = await response.json();
      setSeries(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchSeries();
  }, []);

  const handleClick = async (id: string) => {
    const token = localStorage.getItem("token");
    const res = await fetch(`/api/series/${id}`, {
      headers: token ? { Authorization: `Bearer ${token}` } : {},
    });

    if (!res) return;
    if (res.status === 401) {
      navigate("/login"); 
      return;
    }
    if (res.ok) navigate(`/series/${id}`);
  };

  const handleFilter = () => fetchSeries();

  if (loading) return <p>Loading series...</p>;

  return (
    <div className="series">
      <h1 className="series-pagetitle">Series</h1>

      <div className="series-filters" style={{ marginBottom: "1rem" }}>
        <input 
          type="text" 
          placeholder="Search by title" 
          value={titleFilter} 
          onChange={e => setTitleFilter(e.target.value)} 
          style={{ marginRight: "0.5rem", padding: "0.3rem" }}
        />
        <select value={sortBy} onChange={e => setSortBy(e.target.value)} style={{ marginRight: "0.5rem", padding: "0.3rem" }}>
          <option value="releaseDate">Release Date</option>
          <option value="name">Name</option>
          <option value="author">Author</option>
          <option value="genre">Genre</option>
        </select>
        <select value={isDescending.toString()} onChange={e => setIsDescending(e.target.value === "true")} style={{ marginRight: "0.5rem", padding: "0.3rem" }}>
          <option value="true">Descending</option>
          <option value="false">Ascending</option>
        </select>
        <button onClick={handleFilter} style={{ padding: "0.3rem 0.8rem" }}>Apply</button>
      </div>

      <div className="series-container">
        {series.map((s) => (
          <div key={s.id} className="series-card" onClick={() => handleClick(s.id)} style={{cursor: 'pointer'}}>
            <img src={s.coverImage} alt={s.title} className="series-cover" />
            <h3>{s.title}</h3>
            <p>{s.author}</p>
            <p>Score: {s.score}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default SeriesPage;
