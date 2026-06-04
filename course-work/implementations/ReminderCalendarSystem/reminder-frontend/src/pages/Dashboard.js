import { useEffect, useState } from "react";
import api from "../api/axios";
import CreateEvent from "./CreateEvent";
import "./dashboard.css";

export default function Dashboard() {
  const [events, setEvents] = useState([]);

  const [editingId, setEditingId] = useState(null);
  const [editTitle, setEditTitle] = useState("");

  
  const loadEvents = async () => {
    try {
      const res = await api.get("/Events");
      const data = res.data?.data ?? res.data ?? [];
      setEvents(Array.isArray(data) ? data : []);
    } catch (err) {
      console.log("LOAD ERROR:", err);
      setEvents([]);
    }
  };

  useEffect(() => {
    loadEvents();
  }, []);

  
  const deleteEvent = async (id) => {
    try {
      await api.delete(`/Events/${id}`);
      loadEvents();
    } catch (err) {
      console.log(err);
    }
  };

  
  const startEdit = (event) => {
    setEditingId(event.id);
    setEditTitle(event.title);
  };

  
  const cancelEdit = () => {
    setEditingId(null);
    setEditTitle("");
  };

  
  const saveEdit = async (id) => {
    try {
      await api.put(`/Events/${id}`, {
        title: editTitle,
        description: "",
        eventDate: new Date().toISOString(),
        priority: 1,
        isCompleted: false,
        categoryId: 1
      });

      setEditingId(null);
      setEditTitle("");
      loadEvents();
    } catch (err) {
      console.log("EDIT ERROR:", err.response?.data || err);
    }
  };

  
  const logout = () => {
    localStorage.removeItem("token");
    window.location.href = "/login";
  };

  return (
    <div className="container">

      <div className="topbar">
        <h1>📅 My Events</h1>
        <button onClick={logout}>Logout</button>
      </div>

      <CreateEvent onCreated={loadEvents} />

      <div className="grid">

        {(events?.length ?? 0) === 0 ? (
          <p>No events yet</p>
        ) : (
          events.map(e => (
            <div className="card" key={e.id}>

              {editingId === e.id ? (
                <div>
                  <input
                    value={editTitle}
                    onChange={(e) => setEditTitle(e.target.value)}
                  />

                  <button onClick={() => saveEdit(e.id)}>
                    Save
                  </button>

                  <button onClick={cancelEdit}>
                    Cancel
                  </button>
                </div>
              ) : (
                <>
                  <h3>{e.title}</h3>
                  <p>{e.description}</p>
                  <p>{new Date(e.eventDate).toLocaleString()}</p>

                  <button onClick={() => startEdit(e)}>
                    Edit
                  </button>

                  <button onClick={() => deleteEvent(e.id)}>
                    Delete
                  </button>
                </>
              )}

            </div>
          ))
        )}

      </div>
    </div>
  );
}