import os
import time
import logging
import face_recognition

from PIL import Image
from watchdog.events import FileSystemEventHandler

class ImageHandler(FileSystemEventHandler):
    def __init__(self, watch_directory, output_directory):
        self.watch_directory = watch_directory
        self.output_directory = output_directory

    def process_image(self, image_path):
        try:
            image = face_recognition.load_image_file(image_path)

            face_locations = face_recognition.face_locations(image)

            if not face_locations:
                logging.info(f"No faces found in {image_path}")
                return

            pil_image = Image.open(image_path)

            top, right, bottom, left = face_locations[0]

            face_image = pil_image.crop((left, top, right, bottom))

            image_name_without_extension = os.path.splitext(
                os.path.basename(image_path)
            )[0]

            output_path = os.path.join(
                self.output_directory,
                f"{image_name_without_extension}.png"
            )

            face_image.save(output_path, "PNG")

            logging.info(f"Face from {image_path} saved")

        except Exception as e:

            logging.error(f"Error processing {image_path}: {str(e)}")

    def on_created(self, event):
        if event.is_directory:
            return

        file_path = event.src_path

        file_extension = os.path.splitext(file_path)[1].lower()

        if file_extension in ['.jpg', '.jpeg', '.png']:
            logging.info(f"New image detected: {file_path}")

            time.sleep(1)

            self.process_image(file_path)
