import { useEffect, useState } from "react";
import api from "../api/axios";

export default function CreateEvent({ onCreated }) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [categoryId, setCategoryId] = useState("");
  const [categories, setCategories] = useState([]);

  
  useEffect(() => {
    api.get("/Categories")
      .then(res => {
        const data = res.data?.data ?? res.data ?? [];
        setCategories(Array.isArray(data) ? data : []);
      })
      .catch(err => console.log(err));
  }, []);

  
  const createEvent = async () => {
    try {
      await api.post("/Events", {
        title,
        description,
        eventDate: new Date().toISOString(),
        priority: 1,
        isCompleted: false,
        categoryId: Number(categoryId),
      });

      setTitle("");
      setDescription("");
      setCategoryId("");

      onCreated();
    } catch (err) {
      console.log("CREATE ERROR:", err.response?.data || err);
      alert(err.response?.data?.error || "Failed to create event");
    }
  };

  return (
    <div className="create-box">

      <input
        placeholder="Title"
        value={title}
        onChange={e => setTitle(e.target.value)}
      />

      <input
        placeholder="Description"
        value={description}
        onChange={e => setDescription(e.target.value)}
      />

      <select
        value={categoryId}
        onChange={e => setCategoryId(e.target.value)}
      >
        <option value="">Select category</option>
        {categories.map(c => (
          <option key={c.id} value={c.id}>
            {c.name}
          </option>
        ))}
      </select>

      <button onClick={createEvent}>
        Create Event
      </button>

    </div>
  );
}