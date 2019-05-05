#include <math.h>
#include <Servo.h>

String data = "";
Servo servo;
int servoAngle = 0;
int servoPin = 3;

int buzzer = 12;
int m1in1 = 4;
int m1in2 = 5;
int m1in3 = 6;
int m1in4 = 7;

int m2in1 = 8;
int m2in2 = 9;
int m2in3 = 10;
int m2in4 = 11;

int m1CurrentStep = 0;
int m2CurrentStep = 0;

float Xl;
float Yl;
boolean Xc;
boolean Yc;

void setup() {
  Serial.begin(9600);

  servo.attach(servoPin);

  pinMode(buzzer, OUTPUT);
  
  pinMode(m1in1, OUTPUT);
  pinMode(m1in2, OUTPUT);
  pinMode(m1in3, OUTPUT);
  pinMode(m1in4, OUTPUT);

  pinMode(m2in1, OUTPUT);
  pinMode(m2in2, OUTPUT);
  pinMode(m2in3, OUTPUT);
  pinMode(m2in4, OUTPUT);

  digitalWrite (m1in1, LOW);
  digitalWrite (m1in2, LOW);
  digitalWrite (m1in3, LOW);
  digitalWrite (m1in4, LOW);

  digitalWrite (m2in1, LOW);
  digitalWrite (m2in2, LOW);
  digitalWrite (m2in3, LOW);
  digitalWrite (m2in4, LOW);

  buzzer_tone(750, 250, 3);
  
  //motor1(700, true); // machine: 9k  drawing: 4.5k
  //motor2(1500, true); // machine: 6k drawing 6k
}

void loop() {  
  data = Serial.readString();
  Serial.setTimeout(10);
  if (data.length() > 5) {
    Xl = data.substring(0, data.indexOf("A")+1).toFloat();
    Yl = data.substring(data.indexOf("B")+1, data.indexOf("C")).toFloat();
    if (data.substring(data.indexOf("A")+1, data.indexOf("A")+2).equals("T")) {
      Xc = true;
    } else {
      Xc = false;
    }
    if (data.substring(data.indexOf("C")+1, data.indexOf("C")+2).equals("T")) {
      Yc = true;
    } else {
      Yc = false;
    }
    if (data.substring(data.indexOf("D")+1, data.indexOf("D")+2).equals("T")) {
      pencilUp(true);
      buzzer_tone(600, 100, 1);
    } else {
      pencilUp(false);
    }
    calculateSteps(Xl, Yl, Xc, Yc);
    data = "";
  } else if (data.equals("stop")) {
    // pliiats ülesse ja algusesse
  } else if (data.equals("done")) {
    pencilUp(false);
    buzzer_tone(500, 1000, 3);
  }
}

void calculateSteps (float xl, float yl, boolean xc, boolean yc) {
  int cxl = xl;
  int cyl = yl;
  float rounded;
    
  if (xl > yl && yl > 0) {
    for (int i = 1; i <= yl; i++) {
      rounded = round(cxl/cyl);
      motor2(rounded*9, xc);
      motor1(9, yc);
      cxl -= rounded;
      cyl -= 1;
    }
  } else if (yl == 0) {
    for (int i = 1; i <= xl; i++) {
      motor2(9, xc);
    }
  } else if (xl <= yl && xl > 0) {
      for (int i = 1; i <= xl; i++) {  
        rounded = round(cyl/cxl);   
        motor1(rounded*9, yc);
        motor2(9, xc);
        cyl -= rounded;
        cxl -= 1;
      }
  } else if (xl == 0) {
    for (int i = 1; i <= yl; i++) {
      motor1(9, yc);
    }
  }
  Serial.println("done");
}

void motor1 (int steps, boolean clockwise) {
  for (int i = 1; i <= steps; i++) {
    switch (m1CurrentStep) {
      case 0:
      digitalWrite (m1in1, HIGH);
      digitalWrite (m1in2, HIGH);
      digitalWrite (m1in3, LOW);
      digitalWrite (m1in4, LOW);
      break;
  
      case 1:
      digitalWrite (m1in1, LOW);
      digitalWrite (m1in2, HIGH);
      digitalWrite (m1in3, HIGH);
      digitalWrite (m1in4, LOW);
      break;
  
      case 2:
      digitalWrite (m1in1, LOW);
      digitalWrite (m1in2, LOW);
      digitalWrite (m1in3, HIGH);
      digitalWrite (m1in4, HIGH);
      break;
  
      case 3:
      digitalWrite (m1in1, HIGH);
      digitalWrite (m1in2, LOW);
      digitalWrite (m1in3, LOW);
      digitalWrite (m1in4, HIGH);
      break;
    }
    if (clockwise) {
      m1CurrentStep = (--m1CurrentStep > -1) ? m1CurrentStep : 3;
    } else if (!clockwise) {     
      m1CurrentStep = (++m1CurrentStep < 4) ? m1CurrentStep : 0;
    }
    delay (3);
  }
}

void motor2 (int steps, boolean clockwise) {
  for (int i = 1; i <= steps; i++) {
    switch (m2CurrentStep) {
      case 0:
      digitalWrite (m2in1, HIGH);
      digitalWrite (m2in2, HIGH);
      digitalWrite (m2in3, LOW);
      digitalWrite (m2in4, LOW);
      break;
  
      case 1:
      digitalWrite (m2in1, LOW);
      digitalWrite (m2in2, HIGH);
      digitalWrite (m2in3, HIGH);
      digitalWrite (m2in4, LOW);
      break;
  
      case 2:
      digitalWrite (m2in1, LOW);
      digitalWrite (m2in2, LOW);
      digitalWrite (m2in3, HIGH);
      digitalWrite (m2in4, HIGH);
      break;
  
      case 3:
      digitalWrite (m2in1, HIGH);
      digitalWrite (m2in2, LOW);
      digitalWrite (m2in3, LOW);
      digitalWrite (m2in4, HIGH);
      break;
    }
    if (clockwise) {
      m2CurrentStep = (--m2CurrentStep > -1) ? m2CurrentStep : 3;
    } else if (!clockwise) {     
      m2CurrentStep = (++m2CurrentStep < 4) ? m2CurrentStep : 0;
    }
    delay (3);
  }
}

void buzzer_tone (int hz, int sound_delay, int repeats) {
  for (int i = 0; i < repeats; i++) {
    tone(buzzer, hz);
    delay(sound_delay);
    noTone(buzzer);
    delay(sound_delay);
  }
}

void pencilUp (boolean up) {
  if (up) {
    servo.write(115);
    delay(100);
    servo.write(107);
    delay(100);
  } else {
    servo.write(90);
    delay(100);
  }
}
