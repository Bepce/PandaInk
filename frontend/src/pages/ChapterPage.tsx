import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Chapter } from "../types/Chapter";
import "./ChapterPage.css";

function ChapterPage() {
  const { chapterId } = useParams<{ chapterId: string }>();
  const navigate = useNavigate();

  const [chapter, setChapter] = useState<Chapter | null>(null);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(0);

 useEffect(() => {
  async function fetchChapter() {
    if (!chapterId) return;

    const token = localStorage.getItem("token");

    try {
      const res = await fetch(`/api/chapter/${chapterId}`, {
        headers: token ? { Authorization: `Bearer ${token}` } : {},
      });

      if (res.status === 401) {
        const fallbackSeriesId = "";
        navigate(`/series/${chapter?.seriesId || fallbackSeriesId}`);
        return;
      }

      if (!res.ok) {
        console.error("Failed to fetch chapter");
        setLoading(false);
        return;
      }

      const data: Chapter = await res.json();

      data.content.sort((a, b) => a?.pageNumber - b?.pageNumber);

      setChapter(data);
      setCurrentPage(0);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  }

  fetchChapter();
}, [chapterId, navigate]);

  if (loading) return <p className="loading">Loading chapter...</p>;
  if (!chapter) return <p className="not-found">Chapter not found</p>;

  const totalPages = chapter.content.length;
  const page = chapter.content[currentPage];

  const handlePrev = () => {
    if (currentPage > 0) setCurrentPage(currentPage - 1);
  };

  const handleNext = () => {
    if (currentPage < totalPages - 1) setCurrentPage(currentPage + 1);
  };

  return (
    <div className="chapter-page">
      <h1 className="chapter-title">{chapter.title}</h1>

      <div className="chapter-image-container">
        <img
          src={page?.imageUrl}
          alt={`Page ${page?.pageNumber}`}
          className="chapter-image"
        />
      </div>

      <div className="chapter-pagination">
        <button onClick={handlePrev} disabled={currentPage === 0}>
          Previous
        </button>

        <span>
          Page {page?.pageNumber} / {totalPages}
        </span>

        <button onClick={handleNext} disabled={currentPage === totalPages - 1}>
          Next
        </button>
      </div>
    </div>
  );
}

export default ChapterPage;
