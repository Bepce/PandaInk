import { createContext, useContext, useState, useEffect,  ReactNode } from "react";

interface AuthContextType {
  isLoggedIn: boolean;
  token: string | null;
  setIsLoggedIn: (v: boolean) => void;
  setToken: (token: string | null) => void;
}

export const AuthContext = createContext<AuthContextType>({
  isLoggedIn: false,
  token: null,
  setIsLoggedIn: () => {},
  setToken: () => {},
});


export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [token, setTokenState] = useState<string | null>(null);
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  // Load token from localStorage on mount
  useEffect(() => {
    const savedToken = localStorage.getItem("token");
    if (savedToken) {
      setTokenState(savedToken);
      setIsLoggedIn(true);
    }
  }, []);

  const setToken = (newToken: string | null) => {
    setTokenState(newToken);
    if (newToken) {
      localStorage.setItem("token", newToken);
      setIsLoggedIn(true);
    } else {
      localStorage.removeItem("token");
      setIsLoggedIn(false);
    }
  };
  
  return (
    <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn, token, setToken}}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be inside AuthProvider");
  return context;
};