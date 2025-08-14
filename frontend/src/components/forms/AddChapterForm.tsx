import { useState } from "react";

export function AddChapterForm() {
  const [seriesId, setSeriesId] = useState("");
  const [title, setTitle] = useState("");
  const [chapterNumber, setChapterNumber] = useState(1);
  const [pages, setPages] = useState([{ imageUrl: "", pageNumber: 1 }]);

  function addPage() {
    setPages([...pages, { imageUrl: "", pageNumber: pages.length + 1 }]);
  }

  function removePage(index: number) {
    const updated = pages.filter((_, idx) => idx !== index);
    const renumbered = updated.map((p, idx) => ({ ...p, pageNumber: idx + 1 }));
    setPages(renumbered);
  }

  function handlePageChange(index: number, value: string) {
    const updated = [...pages];
    updated[index] = { ...updated[index], imageUrl: value };
    setPages(updated);
  }

  async function handleSubmit() {
    const token = localStorage.getItem("token");
    if (!token) return;

    const payload = {
    seriesId: seriesId,      
    title : title,
    chapterNumber: Number(chapterNumber),
    content: pages.map(p => ({
      imageUrl: p.imageUrl,         
      pageNumber: Number(p.pageNumber)
    }))
  };
    
    const res = await fetch(`/api/Chapter`, {
      method: "POST",
      headers: { 
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
      },
      body: JSON.stringify(payload)
    });

    if (res.ok) {
      alert("Chapter added!");
      setSeriesId("");
      setTitle("");
      setChapterNumber(1);
      setPages([{ imageUrl: "", pageNumber: 1 }]);
    } else {
      alert("Failed to add chapter");
    }
  }

  return (
    <div style={{ marginTop: "1rem" }}>
      <h2>Add Chapter</h2>
      <input
        value={seriesId}
        onChange={e => setSeriesId(e.target.value)}
        placeholder="Series ID"
      />
      <input
        value={title}
        onChange={e => setTitle(e.target.value)}
        placeholder="Chapter Title"
      />
      <input
        type="number"
        value={chapterNumber}
        onChange={e => setChapterNumber(Number(e.target.value))}
        placeholder="Chapter Number"
        min={1}
      />

      <h3>Pages</h3>
      {pages.map((page, idx) => (
        <div key={idx} style={{ marginBottom: "0.5rem", display: "flex", alignItems: "center" }}>
          <input
            type="text"
            placeholder="Image URL"
            value={page.imageUrl}
            onChange={e => handlePageChange(idx, e.target.value)}
            style={{ marginRight: "0.5rem", flex: 1 }}
          />
          <input
            type="number"
            value={page.pageNumber}
            readOnly
            style={{ width: "70px", marginRight: "0.5rem" }}
          />
          <button type="button" onClick={() => removePage(idx)}>Remove</button>
        </div>
      ))}
      <button type="button" onClick={addPage} style={{ marginRight: "0.5rem" }}>Add Page</button>
      <button type="button" onClick={handleSubmit}>Submit Chapter</button>
    </div>
  );
}
