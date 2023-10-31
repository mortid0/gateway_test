import os
from aiohttp import web

async def handle(request):
    # Generate 10000 random integers as bytes
    return web.Response(text=f"Hello, random numbers: {os.urandom(4000)}")

app = web.Application()
app.router.add_get('/hello', handle)

if __name__ == '__main__':
    web.run_app(app, host='127.0.0.1', port=5000)