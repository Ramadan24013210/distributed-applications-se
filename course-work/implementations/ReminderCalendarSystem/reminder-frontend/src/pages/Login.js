import { useState } from "react";
import api from "../api/axios";

export default function Login({ onLogin }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const res = await api.post("/Auth/login", {
        email,
        password,
      });

      localStorage.setItem("token", res.data.token);

      console.log("LOGIN SUCCESS");

      onLogin(); 
    } catch (err) {
  console.log(err);
  console.log(err.response);

  alert(
    err.response?.data ||
    err.message ||
    "Login failed"
  );
}
  };

  return (
    <div>
      <h2>Login</h2>

      <input
        placeholder="email"
        onChange={(e) => setEmail(e.target.value)}
      />

      <input
        placeholder="password"
        type="password"
        onChange={(e) => setPassword(e.target.value)}
      />

      <button onClick={handleLogin}>Login</button>
    </div>
  );
}