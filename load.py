import requests
import time
import logging
from concurrent.futures import ThreadPoolExecutor

logging.basicConfig(level=logging.DEBUG,datefmt='%H:%M:%S:',format='%(asctime)s %(levelname)s %(message)s')

rate = 5

def make_requests():
    while True:
        r = requests.get('http://localhost:5000/hello')
        if r.status_code == 200:
            logging.debug(r.content)
        time.sleep(1.0 / rate)

with ThreadPoolExecutor(max_workers=16) as executor:
    executor.submit(make_requests)
    executor.submit(make_requests)
    executor.submit(make_requests)
    executor.submit(make_requests)