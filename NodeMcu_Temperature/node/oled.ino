#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <NTPClient.h>
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>

const char *ssid     = "xxxxx";
const char *password = "xxxx";

WiFiUDP ntpUDP;
Adafruit_SSD1306 display = Adafruit_SSD1306(128, 32, &Wire);
NTPClient timeClient(ntpUDP, "europe.pool.ntp.org", 8*3600, 60000);

void setup() {
  Serial.begin(9600);
  WiFi.begin(ssid, password);

  while ( WiFi.status() != WL_CONNECTED ) {
    delay ( 500 );
    Serial.print ( "." );
  }

  timeClient.begin();
  display.begin(SSD1306_SWITCHCAPVCC, 0x3C); 
  display.display(); 
}

void loop() {

   if (Serial.available() > 0)
  {
    timeClient.update();
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(SSD1306_WHITE);
    display.setCursor(0,0);
    display.print("temperature:");
    display.print(Serial.readString());
    display.println(timeClient.getFormattedTime());
    display.display(); 
    delay(500);
  }
}
