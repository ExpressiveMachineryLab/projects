import cv2 as cv
import argparse

from face_detector import Face_Detector

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument("-i", "--image", help="Image")
    args = parser.parse_args()
    image_url = args.image

    if (image_url == None):
        print('need image_url at flag -i')
    else:
        face_cascade = cv.CascadeClassifier(cv.data.haarcascades + 'haarcascade_frontalface_default.xml')
        face_detector = Face_Detector(face_cascade)

        img = cv.imread(image_url)
        gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
        contain_face, faces = face_detector.detect(gray)
        print('detected face' if contain_face else 'no face detected')
        if contain_face:
            for (x,y,w,h) in faces:
                cv.rectangle(img,(x,y),(x+w,y+h),(255,0,0),2)
                roi_gray = gray[y:y+h, x:x+w]
                roi_color = img[y:y+h, x:x+w]
            cv.imshow('img',img)
            cv.waitKey(0)
            cv.destroyAllWindows()