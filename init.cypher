CREATE
  (c1:Car {make: 'Audi', model: 'A4', year: 2010, price: 10000, color: 'black', owner: 'Tomas', vin: '1234567890'}),
  (c2:Car {make: 'BMW', model: 'X5', year: 2015, price: 20000, color: 'white', owner: 'Tomas', vin: '1234567891'}),
  (c3:Car {make: 'Mercedes', model: 'C', year: 2018, price: 30000, color: 'black', owner: 'John', vin: '1234567892'}),
  (c4:Car {make: 'Toyota', model: 'Camry', year: 2019, price: 40000, color: 'white', owner: 'Pete', vin: '1234567893'});

CREATE
  (o1:Owner {name: 'Tomas', age: 30, phone_number: '1234567890'}),
  (o2:Owner {name: 'John', age: 40, phone_number: '1234567891'}),
  (o3:Owner {name: 'Pete', age: 50, phone_number: '1234567892'});

CREATE
  (m1:Manufacturer {make: 'Audi', country: 'Germany', founded: 1909}),
  (m2:Manufacturer {make: 'BMW', country: 'Germany', founded: 1916}),
  (m3:Manufacturer {make: 'Mercedes', country: 'Germany', founded: 1926}),
  (m4:Manufacturer {make: 'Toyota', country: 'Japan', founded: 1937});
