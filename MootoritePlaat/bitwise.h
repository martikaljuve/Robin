
#define set_input(portdir,pin) (portdir &= ~(1<<(pin)))    // pordi pin pööratakse 0-ks - ehk input
#define set_output(portdir,pin) (portdir |= (1<<(pin))) // pordi pin pööratakse 1-ks - ehk output

#define set_toggle(portdir,pin) (portdir ^= (1<<(pin))) // pordi pin pööratakse 1-ks - ehk output

//Same methods as before, but with aliases
#define set_low(portdir,pin) set_input(portdir,pin)  // pordi pin pööratakse 0-ks - ehk input
#define set_high(portdir,pin) set_output(portdir,pin) // pordi pin pööratakse 1-ks - ehk output

