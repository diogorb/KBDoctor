call mailsend -to "%emailto%@%domain%" -from %emailfrom% -ssl -port %port% -auth -smtp %smtp% -sub "Review Commits - KBDoctor" -user %emailfrom% -pass %email_pass% -attach %attach%