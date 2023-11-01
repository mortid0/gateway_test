This is a simple project to load test [YARP](https://github.com/microsoft/reverse-proxy).

To prepare server:
1. Install asyncIO: `pip install aiohttp`
2. Run `python server.py`. This will start an http server on `127.0.0.1:5000`. The `http://127.0.0.1:5000/hello` will return a simple greeting with 1000 random integers.
3. In a separate terminal window start reverse proxy: `dotnet run --framework net7.0`. This will start a proxy on `http://localhost:6000`. It will rate limit all requests to 2000 QPS and requests with dgbxml query parameter to 1 QPS.
   
Use your choise of client (e.g. [Netling](https://github.com/hallatore/Netling)) to create a load on server. 