export async function apiFetch(input: RequestInfo, init?: RequestInit, navigate?: (path: string) => void) {
  try {
    const res = await fetch(input, init);

    if (res.status === 500 && navigate) {
      navigate("/500"); 
      return null; 
    }

    if (!res.ok) {
      // optionally handle other errors
      console.error("API error:", res.status, res.statusText);
    }

    return res;
  } catch (err) {
    console.error("Fetch failed:", err);
    if (navigate) navigate("/500");
    return null;
  }
}