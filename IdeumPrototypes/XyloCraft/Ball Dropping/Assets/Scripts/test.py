import numpy as np
import cv2
import pygame
import sys
import imutils
#for plotting
#import matplotlib.pyplot as plt
#import matplotlib.animation as animation
#%matplotlib inline
#import time
#import pylab as pl
#from IPython import display

print(cv2.__version__)

pygame.init()
screen = pygame.display.set_mode((900,900))

clock= pygame.time.Clock()
pygame.display.set_caption('Xylocode AR')

red = (255,0,0)
green = (0,255,0)
blue = (0,0,255)
yellow = (255,255,0)
darkBlue = (0,0,128)
white = (255,255,255)
black = (0,0,0)
pink = (255,200,200)

#change depending on your machine's settings
webcam = cv2.VideoCapture(0)

frames = []

counter =0
#while loop to constantly read frames
while True:
    
    # check for quit events
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            pygame.quit(); 
            sys.exit();
            
    screen.fill(white)
            
    redbox=None
    greenbox= None
    yellowbox=None
    bluebox= None
    blackbox= None
    _, imageFrame = webcam.read()
    frame = {}
    
    #resizing image to fit my laptop's screen, this is because I am using a camera that can read 4k, will differ depending on your camera
    dimensions = (1200,600)
    imageFrame = cv2.resize(imageFrame,dimensions)
    #hsv conversion
    hsvFrame = cv2.cvtColor(imageFrame, cv2.COLOR_BGR2HSV) 
  
    # red needs two combined masks to work properly  
    #mask1
    #red_lower = np.array([0,120,70], np.uint8) 
    #red_upper = np.array([10,255,255], np.uint8) 
    #red_mask1 = cv2.inRange(hsvFrame, red_lower, red_upper) 
    #mask2
    #red_lower = np.array([170,120,70], np.uint8) 
    #red_upper = np.array([180,255,255], np.uint8) 
    #red_mask2 = cv2.inRange(hsvFrame, red_lower, red_upper)
    #combined
    #red_mask = red_mask1 + red_mask2
    
    red_lower = np.array([140,100,50], np.uint8) 
    red_upper = np.array([180,255,255], np.uint8) 
    red_mask = cv2.inRange(hsvFrame, red_lower, red_upper) 
  
    # range and mask for green
    #green_lower = np.array([25, 52, 72], np.uint8) 
    green_lower = np.array([35, 52, 72], np.uint8) 
    #green_upper = np.array([102, 255, 255], np.uint8) 
    green_upper = np.array([70, 255, 255], np.uint8) 
    green_mask = cv2.inRange(hsvFrame, green_lower, green_upper) 
  
    # range and mask for blue
    blue_lower = np.array([100, 80, 30], np.uint8) 
    blue_upper = np.array([120, 255, 255], np.uint8) 
    blue_mask = cv2.inRange(hsvFrame, blue_lower, blue_upper)
    
    # range and mask for yellow
    yellow_lower = np.array([23,41,133], np.uint8) 
    yellow_upper = np.array([30,255,255], np.uint8) 
    yellow_mask = cv2.inRange(hsvFrame, yellow_lower, yellow_upper) 
    
    black_lower = np.array([0,0,0], np.uint8)
    black_upper = np.array([35,35,60], np.uint8)
    black_mask = cv2.inRange(hsvFrame, black_lower, black_upper)
      
    # Morphological Transform, convolutional kernal to  have averaging dilation for each color 
    # bitwise_and operator between imageFrame and mask determines to detect only that particular color 
    kernal = np.ones((5, 5), "uint8") 
    bkernal = np.ones((21, 21), "uint8")
      
    # red
    red_mask = cv2.dilate(red_mask, kernal) 
    res_red = cv2.bitwise_and(imageFrame, imageFrame,  
                              mask = red_mask) 
      
    # green 
    green_mask = cv2.dilate(green_mask, kernal) 
    res_green = cv2.bitwise_and(imageFrame, imageFrame, 
                                mask = green_mask) 
      
    # blue
    blue_mask = cv2.dilate(blue_mask, kernal) 
    res_blue = cv2.bitwise_and(imageFrame, imageFrame, 
                               mask = blue_mask) 
    
    # yellow 
    yellow_mask = cv2.dilate(yellow_mask, kernal) 
    res_yellow = cv2.bitwise_and(imageFrame, imageFrame, 
                               mask = yellow_mask) 
    
    #black
    black_mask = cv2.dilate(black_mask, bkernal)
    res_black = cv2.bitwise_and(imageFrame, imageFrame,
                               mask = black_mask)
   
    # Red: Creating contour to track red color 
    contours, hierarchy = cv2.findContours(red_mask, 
                                           cv2.RETR_TREE, 
                                           cv2.CHAIN_APPROX_SIMPLE) 
    
    #Red: using rotating bounding box if color detected
    for pic, contour in enumerate(contours): 
        area = cv2.contourArea(contour) 
        if(area > 1200): 
            rect = cv2.minAreaRect(contour) 
            box = cv2.boxPoints(rect)
            redbox = np.int0(box)
            frame['red'] = box
            cv2.drawContours(imageFrame,[redbox],0,(0,0,255),2)   
  
    # Green: Creating contour to track green color 
    contours, hierarchy = cv2.findContours(green_mask, 
                                           cv2.RETR_TREE, 
                                           cv2.CHAIN_APPROX_SIMPLE) 
    #Green: using rotating bounding box if color detected  
    for pic, contour in enumerate(contours): 
        area = cv2.contourArea(contour) 
        if(area > 600): 
            rect = cv2.minAreaRect(contour) 
            box = cv2.boxPoints(rect)
            greenbox = np.int0(box)
            frame['green'] = box
            cv2.drawContours(imageFrame,[greenbox],0,(0,255,0),2)
 
  
    #Blue:  Creating contour to track blue color 
    contours, hierarchy = cv2.findContours(blue_mask, 
                                           cv2.RETR_TREE, 
                                           cv2.CHAIN_APPROX_SIMPLE) 
    #Blue: using rotating bounding box if color detected  
    for pic, contour in enumerate(contours): 
        area = cv2.contourArea(contour) 
        if(area > 600): 
            rect = cv2.minAreaRect(contour) 
            box = cv2.boxPoints(rect)
            bluebox = np.int0(box)
            frame['blue'] = box
            cv2.drawContours(imageFrame,[bluebox],0,(255,0,0),2)
            #x, y, w, h = cv2.boundingRect(contour) 
            #imageFrame = cv2.rectangle(imageFrame, (x, y), 
            #                           (x + w, y + h), 
            #                           (255, 0, 0), 2) 
              
            #cv2.putText(imageFrame, "Blue Colour", (x, y), 
            #            cv2.FONT_HERSHEY_SIMPLEX, 
            #            1.0, (255, 0, 0)) 
            
    # Creating contour to track yellow color 
    contours, hierarchy = cv2.findContours(yellow_mask, 
                                           cv2.RETR_TREE, 
                                           cv2.CHAIN_APPROX_SIMPLE) 
    #Yellow: using rotating bounding box if color detected    
    for pic, contour in enumerate(contours): 
        area = cv2.contourArea(contour) 
        if(area > 600): 
            rect = cv2.minAreaRect(contour) 
            box = cv2.boxPoints(rect)
            yellowbox = np.int0(box)
            frame['yellow'] = box
            cv2.drawContours(imageFrame,[yellowbox],0,(30,255,255),2)
   
    # Creating contour to track black color 
    contours, hierarchy = cv2.findContours(black_mask, 
                                           cv2.RETR_TREE, 
                                           cv2.CHAIN_APPROX_SIMPLE) 
    #black: using rotating bounding box if color detected
    for pic, contour in enumerate(contours):
        area= cv2.contourArea(contour)
        if(area > 1800):
            approx = cv2.approxPolyDP(
                contour, 0.1 * cv2.arcLength(contour, True), True)
            blackbox= np.int0(approx)
            frame['black'] = blackbox
            if len(approx == 3):
                cv2.drawContours(imageFrame, [blackbox], 0, (255,192,203), 3)
    
    #for pic, contour in enumerate(contours): 
        #area = cv2.contourArea(contour) 
    #    area = cv2.arcLength(contour, True)
    #    box = cv2.boxPoints(rect)
    #    blackbox= np.int0(box)
    ##    frame['black'] = box
    #    if len(blackbox) == 3:
    #        coordinates.append([blackbox])
    #        cv2.drawContours(imageFrame, [blackbox], 0, (15, 15, 15), 3)
    
    #plt.clf()    
    
    if redbox is not None:  
        redboxplot = np.zeros((5,2))
        #to turn np array into list of tuples for pygame
        listredpoints= []
        for row in redbox:
            listredpoints.append(tuple(row))
        #to turn the list of points of the box into a 5x2 array for pyplot
        redboxplot[:4,:]=redbox
        redboxplot[-1,:]=redbox[0,:]
        #plt.plot(redboxplot[:,0], redboxplot[:,1], color='red')
        pygame.draw.polygon(screen, red, listredpoints)
        
    if bluebox is not None:
        blueboxplot = np.zeros((5,2))
        #to turn np array into list of tuples for pygame
        listbluepoints= []
        for row in bluebox:
            listbluepoints.append(tuple(row))
        #to turn the list of points of the box into a 5x2 array for pyplot
        blueboxplot[:4,:]=bluebox
        blueboxplot[-1,:]=bluebox[0,:]
        #plt.plot(blueboxplot[:,0], blueboxplot[:,1], color='blue')
        pygame.draw.polygon(screen, blue, listbluepoints)
        
    if greenbox is not None:   
        greenboxplot = np.zeros((5,2))
        #to turn np array into list of tuples for pygame
        listgreenpoints= []
        for row in greenbox:
            listgreenpoints.append(tuple(row))
        #to turn the list of points of the box into a 5x2 array for pyplot
        greenboxplot[:4,:]=greenbox
        greenboxplot[-1,:]=greenbox[0,:]
        #plt.plot(greenboxplot[:,0], greenboxplot[:,1], color='green')
        pygame.draw.polygon(screen, green, listgreenpoints)
        
    if yellowbox is not None:
        yellowboxplot = np.zeros((5,2))
        #to turn np array into list of tuples for pygame
        listyellowpoints= []
        for row in yellowbox:
            listyellowpoints.append(tuple(row))
        #to turn the list of points of the box into a 5x2 array for pyplot
        yellowboxplot[:4,:]=yellowbox
        yellowboxplot[-1,:]=yellowbox[0,:]
        #plt.plot(yellowboxplot[:,0], yellowboxplot[:,1], color='yellow')
        pygame.draw.polygon(screen, yellow, listyellowpoints)
        
    if blackbox is not None:  
        #redboxplot = np.zeros((5,2))
        #to turn np array into list of tuples for pygame
        listblackpoints= []
        for row in blackbox:
            listblackpoints.append(tuple(row))
        #to turn the list of points of the box into a 5x2 array for pyplot
        #redboxplot[:4,:]=redbox
        #redboxplot[-1,:]=redbox[0,:]
        #plt.plot(redboxplot[:,0], redboxplot[:,1], color='red')
        pygame.draw.polygon(screen, black, listblackpoints)
        
    #plt.xlim(0,900)
    #plt.ylim(900,0)
    #ax = plt.gca() #you first need to get the axis handle
    #ax.set_aspect(1) #sets the height to width ratio to 1.5. 

    #display.clear_output()
    #display.display(plt.gcf())   
            
    pygame.display.update()
    
    frames.append(frame)
    
              
    # stop
    cv2.imshow("xylocode AR", imageFrame) 
    if cv2.waitKey(1) & 0xFF == ord('q'):  
        cv2.destroyAllWindows() 
        break
        
#print(frames)
