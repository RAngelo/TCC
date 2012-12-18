#include <SoftwareSerial.h>
 
SoftwareSerial xbee(2, 3); // RX, TX

void setup()  {

 
   // set the data rate for the SoftwareSerial port
   xbee.begin( 9600);
}
 
void loop()  {
  // send character via XBee to other XBee connected to Mac
  // via USB cable
  xbee.print( "Rafael\r\n" );
 
  delay( 1000 );
}
