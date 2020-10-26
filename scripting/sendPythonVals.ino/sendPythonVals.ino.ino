// potentiometer_read.ino
// reads a potentiometer and sends value over serial
int sensorPin1 = A1;  // The sensor on pin A1   
int sensorPin2 = A2;  // The sensor on pin A2   
int sensorPin3 = A3;  // The sensor on pin A3      
int sensorPin4 = A4;  // The sensor on pin A4   
int sensorPin5 = A5;  // The sensor on pin A5   
  
           
int ledPin = 13;     // The LED is connected on pin 13
int sensorValue;     // variable to stores data
String outputValue;

void setup() // runs once when the sketch starts
{
  // make the LED pin (pin 13) an output pin
  pinMode(ledPin, OUTPUT);

  // initialize serial communication
  Serial.begin(9600);
}

void loop() // runs repeatedly after setup() finishes
{
  
  int sensorValue1 = analogRead(sensorPin1);  // read pin A0   
  int sensorValue2 = analogRead(sensorPin2);  // read pin A0   
  int sensorValue3 = analogRead(sensorPin3);  // read pin A0 
  int sensorValue4 = analogRead(sensorPin4);  // read pin A0   
  int sensorValue5 = analogRead(sensorPin5);  // read pin A0   

  
  /*
  int sensorValue1 = 42;   
  int sensorValue2 = 69; 
  int sensorValue3 = 666;  
  */
    
  //outputValue=sensorValue;
  //outputValue = map(sensorValue, 0, 1023, 0, 255);

  outputValue=String(String(sensorValue1) + "  " + String(sensorValue2) + "  " + String(sensorValue3) + "  " + String(sensorValue4) + "  " + String(sensorValue5));
  
  //Serial.print(sensorValue1);         // send data to serial
  //Serial.print(sensorValue2);         // send data to serial
  //Serial.println(sensorValue3);         // send data to serial
  Serial.println(outputValue);
/*
  if (outputValue < 500) {            // less than 500?
    digitalWrite(ledPin, LOW); }     // Turn the LED off

  else {                               // greater than 500?
    digitalWrite(ledPin, HIGH); }     // Keep the LED on
   */

  delay(1);             // Pause 100 milliseconds
}
