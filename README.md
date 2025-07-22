# 📘 Smart Task Orchestrator

An AI-powered personal task and notes manager built with **.NET 9 Web API**, designed for developers who want to stay organized with minimal effort. This backend service captures tasks, summarizes content using AI, and helps categorize and prioritize intelligently.

> A personal productivity engine for engineers — blending structured tasks with organic dev notes, powered by AI insight.

---

## 🧠 Why This Project Exists

As a backend engineer, it's easy to drown in ideas, bugs, and feature requests. This orchestrator gives you a private, extensible API to:

- Collect thoughts, todos, dev logs, and plans from anywhere (e.g., Postman, terminal, future Slack bot)
- Summarize or tag long entries with AI
- Search/filter through your work like a pro

It also serves as a practical playground to learn and demonstrate:

- .NET 9 Web API development
- AI integration (OpenAI / HuggingFace)
- Clean architecture and scaling practices
- Product thinking and SDLC application

---

## 🔧 Project Roadmap (SDLC Inspired)

### 🔍 Phase 1: Requirements & Planning

- ✅ Define MVP scope (CRUD for tasks & dev notes, AI summarization)
- ✅ Select tech stack (.NET 9, EF Core, SQLite/Postgres, Swagger)
- ⬜ Set up logging and diagnostics
- ⬜ Add unit tests and basic CI

### 🧱 Phase 2: MVP Development

- ✅ Basic API routes: `POST /tasks`, `GET /tasks`, `GET /tasks/{id}`
- ✅ Models for tasks and dev notes
- ⬜ AI Service to summarize/label notes
- ⬜ Background job for AI processing
- ⬜ Optional frontend (React/Next.js)

### 🚀 Phase 3: Iteration & Scaling

- ⬜ User Auth (JWT or OAuth2)
- ⬜ Export/Import tasks & notes
- ⬜ Webhooks / Slack / GitHub integration
- ⬜ Multi-user support (multi-tenancy)
- ⬜ Dockerize and deploy

---

## 🛠 Tech Stack

| Layer           | Tech                                   |
| --------------- | -------------------------------------- |
| Backend API     | .NET 9 Web API                         |
| ORM / DB        | Entity Framework Core + SQLite (start) |
| AI Integration  | HuggingFace Inference API (free)       |
| API Testing     | Swagger UI / Postman                   |
| Logging / Jobs  | Serilog / Hangfire (future)            |
| Version Control | Git + GitHub                           |

---

## 📦 Installation & Setup

### Prerequisites

- .NET 9 SDK: [Download](https://dotnet.microsoft.com/en-us/download)
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

📬 API Endpoints (MVP)

| Method | Endpoint       | Description          |
| ------ | -------------- | -------------------- |
| POST   | /tasks         | Create a new task    |
| GET    | /tasks         | Get all tasks        |
| GET    | /tasks/{id}    | Get a task by ID     |
| POST   | /devnotes      | Create a dev note    |
| GET    | /devnotes      | List all dev notes   |
| GET    | /devnotes/{id} | Get a dev note by ID |

🔮 Future Enhancements

- Task & DevNote prioritization with AI
- Tag extraction & trend insights
- Email/Voice note ingestion
- Calendar sync (Google/Outlook)
- Slack bot to log tasks & notes
- Browser extension / Git hook integrations

🤖 AI Integration (Planned)

- HuggingFace summarization model (free tier)
- Optional: OpenAI GPT for tag extraction / prioritization
- AI endpoints exposed as /ai/notes/enhance and /ai/tasks/suggest

🧠 Learning Outcomes

- Master .NET 9 Web API structure
- Build scalable backend systems from scratch
- Integrate AI into developer workflows
- Apply SDLC thinking to a real-world dev tool

✍️ Author

> David. Experienced Backend Engineer, building with .NET to stay organized, level up, and share tools that scratch real itches.

"Great software starts as a scratch for your own itch. Then you give it legs."
