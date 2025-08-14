import { useState } from "react";
import { useNavigate } from "react-router-dom";

function AdminAddSeriesPage() {
  const navigate = useNavigate();
  const [title, setTitle] = useState("");
  const [genre, setGenre] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const [author, setAuthor] = useState("");
  const [coverImage, setCoverImage] = useState("");

  async function handleAddSeries(e: React.FormEvent) {
    e.preventDefault();
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
      return;
    }

    const payload = { title, genre, releaseDate, author, coverImage };

    try {
      const res = await fetch("/api/series", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
      });

      if (res.ok) {
        navigate("/admin/series-list");
      } else {
        console.error("Failed to add series");
      }
    } catch (err) {
      console.error(err);
    }
  }

  return (
    <div className="card" style={{ padding: "1rem", maxWidth: "500px", margin: "0 auto" }}>
      <h2>Add New Series</h2>
      <form onSubmit={handleAddSeries} style={{ display: "flex", flexDirection: "column", gap: "0.75rem" }}>
        <input type="text" placeholder="Title" value={title} onChange={(e) => setTitle(e.target.value)} required />
        <input type="text" placeholder="Genre" value={genre} onChange={(e) => setGenre(e.target.value)} required />
        <input type="date" value={releaseDate} onChange={(e) => setReleaseDate(e.target.value)} required />
        <input type="text" placeholder="Author" value={author} onChange={(e) => setAuthor(e.target.value)} required />
        <input type="text" placeholder="Cover Image URL" value={coverImage} onChange={(e) => setCoverImage(e.target.value)} required />
        <button type="submit" className="add-btn">Add Series</button>
      </form>
    </div>
  );
}

export default AdminAddSeriesPage;
