import { useState } from "react";

export function AddSeriesForm() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [genre, setGenre] = useState("");
  const [coverImage, setCoverImage] = useState("");
  const [author, setAuthor] = useState("");
  const [releaseDate, setReleaseDate] = useState("");
  const score =  "0";

  async function handleSubmit() {
    const token = localStorage.getItem("token");
    if (!token) return;

    const res = await fetch("/api/series", {
      method: "POST",
      headers: { 
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
      },
      body: JSON.stringify({ title, description, genre, coverImage, author, releaseDate, score})
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
      <input value={description} onChange={e => setDescription(e.target.value)} placeholder="Description" />
      <input value={author} onChange={e => setAuthor(e.target.value)} placeholder="Author" />
      <input value={coverImage} onChange={e => setCoverImage(e.target.value)} placeholder="Cover url" />
      <input value={releaseDate} onChange={e => setReleaseDate(e.target.value)} placeholder="Release Date" />
      <input value={genre} onChange={e => setGenre(e.target.value)} placeholder="Genre" />
      <button onClick={handleSubmit}>Submit</button>
    </div>
  );
}
