@rem auto create by AVRUBD at 16.10.2010 17:39:12
avr-gcc.exe  -mmcu=atmega324p -Wall -gdwarf-2  -Os -fsigned-char -MD -MP  -c  bootldr.c
avr-gcc.exe -mmcu=atmega324p  -Wl,-section-start=.text=0x7000 bootldr.o     -o Bootldr.elf
avr-objcopy -O ihex -R .eeprom  Bootldr.elf Bootldr.hex
@pause
