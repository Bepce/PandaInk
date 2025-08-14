// src/pages/Error404.tsx
import "./NotFoundPage.css";

export default function Error404() {
  return (
    <div className="error-page">
      <h1>404 - Page Not Found</h1>
      <p>Sorry, the page you are looking for does not exist.</p>
      <img src="https://i.pinimg.com/736x/f5/da/90/f5da90e555218741ddc429350c0be062.jpg" alt="Lost" className="error-gif" />
    </div>
  );
}
