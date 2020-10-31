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
   
  int sensorValue5 = analogRead(sensorPin5);  // read pin A5   


    
  //outputValue=sensorValue;
  //outputValue = map(sensorValue, 0, 1023, 0, 255);

  outputValue=String(sensorValue5);
  
  Serial.println(outputValue);
/*
  if (outputValue < 500) {            // less than 500?
    digitalWrite(ledPin, LOW); }     // Turn the LED off

  else {                               // greater than 500?
    digitalWrite(ledPin, HIGH); }     // Keep the LED on
   */
  if (sensorValue5<200) {
    delay(1);             // Pause 1 milliseconds
  } else {
    delay(1000);             // Pause 1000 milliseconds
  } 

  
}
