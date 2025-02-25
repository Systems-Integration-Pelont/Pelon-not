FROM python:3.13-slim

WORKDIR /app

RUN apt-get update && apt-get install -y \
    cmake \
    build-essential \
    libopenblas-dev \
    liblapack-dev \
    libx11-dev \
    && rm -rf /var/lib/apt/lists/*

COPY pyproject.toml ./

RUN pip install uv
RUN uv venv
RUN uv pip install -r pyproject.toml

COPY . .

CMD ["bash", "-c", "source ./.venv/bin/activate && python main.py"]
