import { useState } from "react";
import { AddSeriesForm } from "../components/forms/AddSeriesForm";
import { AddChapterForm } from "../components/forms/AddChapterForm";
import { useNavigate } from "react-router-dom";
  
function getUserRole(): string | null {
  const token = localStorage.getItem("token");
  if (!token) return null;

  try {
    const payload = JSON.parse(atob(token.split(".")[1]));
    return payload.role || null; 
  } catch {
    return null;
  }
}


function AdminPage() {
  const [activeForm, setActiveForm] = useState<"series" | "chapter" | null>(null);
  const role = getUserRole();
  const navigate = useNavigate();
  
  if(role !== "admin"){
    return(
      <p>You do not have permission to access this page.</p>
    );
  }
  navigate('/')

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
