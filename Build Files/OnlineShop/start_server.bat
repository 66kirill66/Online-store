@echo off
cd /d %~dp0
start python -m http.server 8000
timeout /t 2 > nul
cd /d C:\Windows\System32
start "" "http://localhost:8000