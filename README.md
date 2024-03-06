# PilkStore
 
## Purpose
PilkStore was a proof-of-concept project created to understand the .NET MAUI Framework and provide a rudimentary example of a multi-platform app connected to a simple REST service with a single codebase. It keeps track of things (Pilk) and where you store them (Locations). 

**This application contains no authentication or security measures and is not production-ready. Use at your own risk.**

## Installation & Usage

### From Source
You will need:
- Visual Studio 2022 with the ".NET Multi-platform App UI development" Workload installed
- Some kind of git client
- Python 3.10 - 3.12

1. Clone the repository to your preferred directory.
2. Navigate to the `server` folder
3. Run the following to install the server dependancies
```
py -m venv .env
.\.env\Scripts\activate
pip install -r requirements.txt
```

Run the server from the run_server.bat script

Build and run the client on your preferred device from the VS solution.
