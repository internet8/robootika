const int buzzerPin = 2; 
const int stepPin1 = 3; 
const int dirPin1 = 4; 
const int stepPin2 = 5; 
const int dirPin2 = 6; 
const int stepPin3 = 7; 
const int dirPin3 = 8; 
const int stepPin4 = 9; 
const int dirPin4 = 10; 
const int stepPin5 = 11; 
const int dirPin5 = 12; 
String in = "";
 
void setup() {
  
  // Sets the two pins as Outputs
  pinMode(stepPin1,OUTPUT); 
  pinMode(dirPin1,OUTPUT);
  pinMode(stepPin2,OUTPUT); 
  pinMode(dirPin2,OUTPUT);
  pinMode(stepPin3,OUTPUT); 
  pinMode(dirPin3,OUTPUT);
  pinMode(stepPin4,OUTPUT); 
  pinMode(dirPin4,OUTPUT);
  pinMode(stepPin5,OUTPUT); 
  pinMode(dirPin5,OUTPUT);
  Serial.begin(9600);
}
void loop() {
  in = Serial.readString();
  Serial.setTimeout(10); 
  //Serial.print("Received command: " + in);
  in.trim();
  if (in == "red") {
    motorTurn(9, 10, true);
  } else if (in == "green") {
    motorTurn(3, 4, true);
  } else if (in == "blue") {
    motorTurn(7, 8, true);
  } else if (in == "orange") {
    motorTurn(5, 6, true);
  } else if (in == "yellow") {
    motorTurn(11, 12, true);
  } else if (in == "'red") {
    motorTurn(9, 10, false);
  } else if (in == "'green") {
    motorTurn(3, 4, false);
  } else if (in == "'blue") {
    motorTurn(7, 8, false);
  } else if (in == "'orange") {
    motorTurn(5, 6, false);
  } else if (in == "'yellow") {
    motorTurn(11, 12, false);
  }

  /*for (int i = 0; i < 100; i++) {
    for (int j = 2; j <= 10; j+=2) {
      motorTurn(j, j+1, true);
      delay(150);
    }
  }*/
}

void motorTurn (int stepPin, int dirPin, bool high) {
  if (high) {
    digitalWrite(dirPin,HIGH);
  } else {
    digitalWrite(dirPin,LOW);
  }
  
  for (int i = 0; i < 50; i++) {
    digitalWrite(stepPin,HIGH); 
    delayMicroseconds(6000); 
    digitalWrite(stepPin,LOW); 
    delayMicroseconds(6000); 
  }

  if (high) {
    digitalWrite(dirPin,LOW);
  } else {
    digitalWrite(dirPin,HIGH);
  }
  delay(200);
  Serial.println("done");
  /*for (int i = 0; i < 5; i++) {
    digitalWrite(stepPin,HIGH); 
    delayMicroseconds(3000); 
    digitalWrite(stepPin,LOW); 
    delayMicroseconds(3000); 
  }*/
}
