import { useState } from "react";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";

function App() {
  const [loggedIn, setLoggedIn] = useState(
    !!localStorage.getItem("token")
  );

  const handleLogin = () => {
    setLoggedIn(true);
  };

  return (
    <>
      {!loggedIn ? (
        <Login onLogin={handleLogin} />
      ) : (
        <Dashboard />
      )}
    </>
  );
}

export default App;