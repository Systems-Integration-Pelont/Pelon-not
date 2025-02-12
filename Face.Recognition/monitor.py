import os
import time
import logging

from watchdog.observers import Observer
from image_handler import ImageHandler

SHARED_DIR = "/app_data"

class Monitor:
    def __init__(self):
        logging.basicConfig(
            level=logging.INFO,
            format='%(asctime)s - %(message)s',
            datefmt='%Y-%m-%d %H:%M:%S'
        )

        self.watch_directory = os.path.join(SHARED_DIR, "images")
        self.output_directory = os.path.join(SHARED_DIR, "faces")

        os.makedirs(self.watch_directory, exist_ok=True)
        os.makedirs(self.output_directory, exist_ok=True)

        self.event_handler = ImageHandler(self.watch_directory, self.output_directory)

    def start_monitoring(self):
        observer = Observer()
        observer.schedule(self.event_handler, self.watch_directory, recursive=False)
        observer.start()

        logging.info(f"Started monitoring {self.watch_directory}")
        logging.info(f"Extracted faces will be saved to {self.output_directory}")

        try:
            while True:
                time.sleep(1)
        except KeyboardInterrupt:
            observer.stop()
            logging.info("Stopping monitoring...")

        observer.join()
