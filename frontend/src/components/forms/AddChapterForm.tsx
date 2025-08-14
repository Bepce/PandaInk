import { useState } from "react";

export function AddChapterForm() {
  const [seriesId, setSeriesId] = useState("");
  const [title, setTitle] = useState("");

  async function handleSubmit() {
    const token = localStorage.getItem("token");
    if (!token) return;

    const res = await fetch(`/api/series/${seriesId}/chapters`, {
      method: "POST",
      headers: { 
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
      },
      body: JSON.stringify({ title })
    });

    if (res.ok) {
      alert("Chapter added!");
      setSeriesId(""); setTitle("");
    } else {
      alert("Failed to add chapter");
    }
  }

  return (
    <div style={{ marginTop: "1rem" }}>
      <h2>Add Chapter</h2>
      <input value={seriesId} onChange={e => setSeriesId(e.target.value)} placeholder="Series ID" />
      <input value={title} onChange={e => setTitle(e.target.value)} placeholder="Chapter Title" />
      <button onClick={handleSubmit}>Submit</button>
    </div>
  );
}
