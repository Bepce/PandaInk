import { useState } from "react";
import { AddSeriesForm } from "../components/forms/AddSeriesForm";
import { AddChapterForm } from "../components/forms/AddChapterForm";

function AdminPage() {
  const [activeForm, setActiveForm] = useState<"series" | "chapter" | null>(null);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Admin Panel</h1>

      <div style={{ marginBottom: "1rem" }}>
        <button onClick={() => setActiveForm("series")}>Add Series</button>
        <button onClick={() => setActiveForm("chapter")}>Add Chapter</button>
      </div>

      {activeForm === "series" && <AddSeriesForm />}
      {activeForm === "chapter" && <AddChapterForm />}
    </div>
  );
}

export default AdminPage;
