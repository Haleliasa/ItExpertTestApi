select
	cl."ClientName",
	(
		select count(1)
		from "ClientContacts" cont
		where cont."ClientId" = cl."Id"
	) as "ContactCount"
from "Clients" cl
