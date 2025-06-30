# 📘 Smart Task Orchestrator

An AI-powered personal task and notes manager built with **.NET 8 Web API**, designed for developers who want to stay organized with minimal effort. This backend service captures tasks, summarizes content using AI, and helps categorize and prioritize intelligently.

> A hands-on learning project to master .NET backend development, follow good software development life cycle (SDLC) practices, and integrate modern AI tools.

---

## 🧠 Why This Project Exists

As a backend engineer, it's easy to drown in ideas, tasks, and feature requests. This orchestrator gives you a private, extensible API to:

- Collect thoughts, todos, and plans from anywhere (e.g., Postman, terminal, future Slack bot)
- Summarize or tag long entries with AI
- Search/filter through your work like a pro

It also serves as a playground to learn:

- .NET 8 Web API development
- AI integration (OpenAI / HuggingFace)
- Clean architecture and scaling practices

---

## 🔧 Project Roadmap (SDLC Inspired)

### 🔍 Phase 1: Requirements & Planning

- ✅ Define MVP scope (CRUD for tasks, AI summarization)
- ✅ Select tech stack (.NET 8, EF Core, SQLite/Postgres, Swagger)
- ⬜ Set up logging and diagnostics
- ⬜ Add unit tests and basic CI

### 🧱 Phase 2: MVP Development

- ✅ Basic API routes: `POST /tasks`, `GET /tasks`, `GET /tasks/{id}`
- ⬜ AI Service to summarize/label tasks
- ⬜ Background job for AI processing
- ⬜ Optional frontend (React/Next.js)

### 🚀 Phase 3: Iteration & Scaling

- ⬜ User Auth (JWT or OAuth2)
- ⬜ Export/Import tasks
- ⬜ Webhooks / Slack / GitHub integration
- ⬜ Multiple user support (multi-tenancy)
- ⬜ Dockerize and deploy

---

## 🛠 Tech Stack

| Layer           | Tech                                   |
| --------------- | -------------------------------------- |
| Backend API     | .NET 8 Web API                         |
| ORM / DB        | Entity Framework Core + SQLite (start) |
| AI Integration  | HuggingFace Inference API (free)       |
| API Testing     | Swagger UI / Postman                   |
| Logging / Jobs  | Serilog / Hangfire (future)            |
| Version Control | Git + GitHub                           |

---

## 📦 Installation & Setup

### Prerequisites

- .NET 8 SDK: [Download](https://dotnet.microsoft.com/en-us/download)
- VS Code (recommended)
- Git CLI

### Running Locally

```bash
# Clone the repo
git clone https://github.com/YOUR_USERNAME/smart-task-orchestrator.git
cd smart-task-orchestrator

# Restore dependencies and run migrations
dotnet restore
dotnet ef database update

# Run the project
dotnet run
```

Visit `http://localhost:5000/swagger` to test via Swagger UI.

---

## 📬 API Endpoints (MVP)

| Method | Endpoint    | Description       |
| ------ | ----------- | ----------------- |
| POST   | /tasks      | Create a new task |
| GET    | /tasks      | Get all tasks     |
| GET    | /tasks/{id} | Get a task by ID  |

---

## 🔮 Future Enhancements

- Task prioritization with AI
- Email ingestion
- Calendar sync (Google/Outlook)
- Slack bot to log tasks
- Voice note to task (using Whisper)

---

## 🤖 AI Integration (Planned)

- HuggingFace summarization model (free)
- Optional: OpenAI GPT for tag extraction / prioritization

---

## 🧠 Learning Outcomes

- Master .NET 8 Web API structure
- Build scalable backend systems from scratch
- Integrate AI into practical workflows
- Apply SDLC thinking to personal projects

---

## 📜 License

MIT License. Build and evolve freely.

---

## ✍️ Author

**David** – Experienced Backend Engineer, building with .NET for practical backend projects I'll personally use.

> "Great software starts as a scratch for your own itch. Then you give it legs."
