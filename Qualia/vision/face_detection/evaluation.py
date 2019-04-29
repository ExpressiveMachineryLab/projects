import cv2 as cv
import glob

from face_detector import Face_Detector
from progress import Progress

def display(img, faces):
    for (x,y,w,h) in faces:
        cv.rectangle(img,(x,y),(x+w,y+h),(255,0,0),2)
        # roi_gray = gray[y:y+h, x:x+w]
        # roi_color = img[y:y+h, x:x+w]

    cv.imshow('img',img)
    cv.waitKey(0)
    cv.destroyAllWindows()

def evaluate(face_detector):
    # stats variables
    face_total = 0
    face_correct = 0
    face_failed_list = []
    food_total = 0
    food_correct = 0
    food_failed_list = []

    # data paths
    base_path = '../data/'
    face_data_paths = ['Aberdeen/*']
    food_data_paths = ['google_salty_food/*', 'google_sweet_food/*']

    # face images
    for face_data_path in face_data_paths:
        all_files = list(glob.iglob(base_path + face_data_path))
        for i, filepath in enumerate(all_files):
            Progress(i, len(all_files), title=face_data_path.split('/')[0])
            try:
                img = cv.imread(filepath)
                gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
                if (face_detector.detect(gray)[0]):
                    face_correct += 1
                else:
                    face_failed_list.append(filepath)
                face_total += 1
            except:
                print('\nfail to load image')
        print('') 
    
    # food images
    for food_data_path in food_data_paths:
        all_files = list(glob.iglob(base_path + food_data_path))
        for i, filepath in enumerate(all_files):
            Progress(i, len(all_files), title=food_data_path.split('/')[0])
            try:
                img = cv.imread(filepath)
                gray = cv.cvtColor(img, cv.COLOR_BGR2GRAY)
                if (not face_detector.detect(gray)[0]):
                    food_correct += 1
                else:
                    food_failed_list.append(filepath)
                food_total += 1
            except:
                print('\nfail to load image')
        print('')

    # print out results
    print('overall accuracy: ', float(face_correct + food_correct)/float(face_total + food_total))
    print('face images accuracy: ', float(face_correct)/float(face_total))
    print('food images accuracy: ', float(food_correct)/float(food_total))
    # print('failed face images: ', face_failed_list)
    # print('failed food images: ', food_failed_list)


if __name__ == '__main__':
    face_cascade = cv.CascadeClassifier(cv.data.haarcascades + 'haarcascade_frontalface_default.xml')
    face_detector = Face_Detector(face_cascade)
    evaluate(face_detector)