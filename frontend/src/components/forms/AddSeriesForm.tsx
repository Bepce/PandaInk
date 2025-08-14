import { useState } from "react";

export function AddSeriesForm() {
  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [genre, setGenre] = useState("");

  async function handleSubmit() {
    const token = localStorage.getItem("token");
    if (!token) return;

    const res = await fetch("/api/series", {
      method: "POST",
      headers: { 
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
      },
      body: JSON.stringify({ title, author, genre })
    });

    if (res.ok) {
      alert("Series added!");
      setTitle(""); setAuthor(""); setGenre("");
    } else {
      alert("Failed to add series");
    }
  }

  return (
    <div style={{ marginTop: "1rem" }}>
      <h2>Add Series</h2>
      <input value={title} onChange={e => setTitle(e.target.value)} placeholder="Title" />
      <input value={author} onChange={e => setAuthor(e.target.value)} placeholder="Author" />
      <input value={genre} onChange={e => setGenre(e.target.value)} placeholder="Genre" />
      <button onClick={handleSubmit}>Submit</button>
    </div>
  );
}
