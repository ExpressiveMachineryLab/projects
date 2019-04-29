class Face_Detector:
    def __init__(self, face_cascade):
        self.face_cascade = face_cascade

    def detect(self, image):
        faces = self.face_cascade.detectMultiScale(image, 1.3, 5)
        return len(faces) != 0, faces