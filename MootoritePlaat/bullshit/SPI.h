#include "Main.h"

void InitSPI(void);

void WriteByteSPI(unsigned char byte);

char ReadByteSPI(char addr);

uint8_t spi_transmit_byte(uint8_t byte);

void checkSPI(void);
