Имеется 2 интерфейса Ireposytory и ITrade.

1) Ireposytory используется для того, чтобы в случае, когда пользователь захочет записывать данные в какое-нибудь другое хранилище, к примеру в файл или другую таблицу, Он должен создать новый класс, которыый имплементирует этот интерйес и после этого его отправить в конструктор как параметр и реализовать метод SaveData. Но тогда возникает другая проблема, ведь может быть такое, что Trade поменяется и пользователь захочет записывать в другом формате. Поэтому возникает и ITrade
2) Itrade - это интерфейс, который имеет единственный метод, а именно FillData. Поэтому при создании нового Trade пользователю надо будет создать новый класс и отправить его в конструктор.