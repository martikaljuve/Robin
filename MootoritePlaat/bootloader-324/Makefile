# Makefile for ATmegaBOOT
# E.Lins, 18.7.2005
# $Id$


# program name should not be changed...
PROGRAM    = ATmegaBOOT_324p

# enter the target CPU frequency
AVR_FREQ   = 20000000L

MCU_TARGET = atmega324p

#  BOOTSZ1=1, BOOTSZ0=0 (see pg 291 of doc8011)
# This is the start of the boot loader flash section times two
# this corresponds to BOOTSZ1=1, BOOTSZ0=0 (remember to set this in the fuses)
LDSECTION  = --section-start=.text=0x7800

OBJ        = $(PROGRAM).o
OPTIMIZE   = -Os -fno-inline

DEFS       = -DF_CPU=$(AVR_FREQ) -DMAX_TIME_COUNT=4000000L
LIBS       = 

CC         = avr-gcc


# Override is only needed by avr-lib build system.

override CFLAGS        = -Wall $(OPTIMIZE) -mmcu=$(MCU_TARGET) $(DEFS)
override LDFLAGS       = -Wl,$(LDSECTION)
#override LDFLAGS       = -Wl,-Map,$(PROGRAM).map,$(LDSECTION)

OBJCOPY        = avr-objcopy
OBJDUMP        = avr-objdump

all: $(PROGRAM).hex $(PROGRAM).lst

dist: $(PROGRAM).hex
	rm -rf *.o *.elf *.lst *.map *.sym *.lss *.eep *.srec *.bin *~

$(PROGRAM).hex: $(PROGRAM).elf
	$(OBJCOPY) -j .text -j .data -O ihex $< $@
	
$(PROGRAM).elf: $(OBJ)
	$(CC) $(CFLAGS) $(LDFLAGS) -o $@ $^ $(LIBS)
	
$(OBJ): $(PROGRAM).c
	avr-gcc $(CFLAGS) $(LDFLAGS) -c -g -O2 -Wall -mmcu=$(MCU_TARGET) $(PROGRAM).c -o $(PROGRAM).o

%.lst: %.elf
	$(OBJDUMP) -h -S $< > $@

%.srec: %.elf
	$(OBJCOPY) -j .text -j .data -O srec $< $@

%.bin: %.elf
	$(OBJCOPY) -j .text -j .data -O binary $< $@

clean:
	rm -rf *.o *.elf *.lst *.map *.sym *.lss *.eep *.srec *.bin *.hex *~
	
