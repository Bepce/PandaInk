import React from "react";
import { useNavigate } from "react-router-dom";
import "./ServerErrorPage.css"; 

function ServerErrorPage() {
  const navigate = useNavigate();

  return (
    <div className="server-error-page" style={{ textAlign: "center", padding: "2rem" }}>
      <h1>500 - Internal Server Error</h1>
      <p>Oops! Something went terribly wrong on the server.</p>
      <img
        src="https://media.tenor.com/qg324pNzm50AAAAM/server-is-fine-burn.gif"
        alt="Server on fire"
        style={{ maxWidth: "100%", height: "auto", margin: "2rem 0" }}
      />
      <button onClick={() => navigate("/")}>Go Home</button>
    </div>
  );
}

export default ServerErrorPage;
