
// Delete nodes before creating
MATCH (n) DETACH DELETE n;

// Create nodes
CREATE
  (l1:Location {id: 1, name: 'Location 1'}),
  (l2:Location {id: 2, name: 'Location 2'}),
  (l3:Location {id: 3, name: 'Location 3'}),
  (l4:Location {id: 4, name: 'Location 4'}),
  (l5:Location {id: 5, name: 'Location 5'}),
  (l6:Location {id: 6, name: 'Location 6'}),
  (l7:Location {id: 7, name: 'Location 7'}),

  (l1)-[:ROAD {distance: 10}]->(l2),
  (l2)-[:ROAD {distance: 20}]->(l3),
  (l1)-[:ROAD {distance: 20}]->(l3),
  (l2)-[:ROAD {distance: 30}]->(l4),
  (l3)-[:ROAD {distance: 30}]->(l4),
  (l4)-[:ROAD {distance: 40}]->(l5),
  (l5)-[:ROAD {distance: 50}]->(l6),
  (l6)-[:ROAD {distance: 60}]->(l7),

  (l2)-[:ROAD {distance: 10}]->(l1),
  (l3)-[:ROAD {distance: 20}]->(l2),
  (l4)-[:ROAD {distance: 30}]->(l3),
  (l5)-[:ROAD {distance: 20}]->(l4),
  (l6)-[:ROAD {distance: 10}]->(l5),
  (l7)-[:ROAD {distance: 30}]->(l6),

  (o1:Order {id: 1, name: 'Order 1'})-[:HAS_LOCATION]->(l1),
  (o2:Order {id: 2, name: 'Order 2'})-[:HAS_LOCATION]->(l1),
  (o3:Order {id: 3, name: 'Order 3'})-[:HAS_LOCATION]->(l2),
  (o4:Order {id: 4, name: 'Order 4'})-[:HAS_LOCATION]->(l1),
  (o5:Order {id: 5, name: 'Order 5'})-[:HAS_LOCATION]->(l2),
  (o6:Order {id: 6, name: 'Order 6'})-[:HAS_LOCATION]->(l3),
  (o7:Order {id: 7, name: 'Order 7'})-[:HAS_LOCATION]->(l5),

  (cu1:Customer {id: 1, name: 'Customer 1'})-[:HAS_LOCATION]->(l4),
  (cu2:Customer {id: 2, name: 'Customer 2'})-[:HAS_LOCATION]->(l4),
  (cu3:Customer {id: 3, name: 'Customer 3'})-[:HAS_LOCATION]->(l5),
  (cu4:Customer {id: 4, name: 'Customer 4'})-[:HAS_LOCATION]->(l5),
  (cu5:Customer {id: 5, name: 'Customer 5'})-[:HAS_LOCATION]->(l6),

  (cu1)-[:ORDERED]->(o1),
  (cu1)-[:ORDERED]->(o2),
  (cu1)-[:ORDERED]->(o3),
  (cu2)-[:ORDERED]->(o4),
  (cu3)-[:ORDERED]->(o5),
  (cu3)-[:ORDERED]->(o6),
  (cu4)-[:ORDERED]->(o7),

  (c1:Courier {id: 1, name: 'Courier 1'}),
  (c2:Courier {id: 2, name: 'Courier 2'}),
  (c3:Courier {id: 3, name: 'Courier 3'}),

  (c1)-[:ASSIGNED_TO_DELIVER]->(o1),
  (c1)-[:ASSIGNED_TO_DELIVER]->(o2),
  (c1)-[:ASSIGNED_TO_DELIVER]->(o3),
  (c2)-[:ASSIGNED_TO_DELIVER]->(o4),
  (c3)-[:ASSIGNED_TO_DELIVER]->(o5),
  (c3)-[:ASSIGNED_TO_DELIVER]->(o6),
  (c3)-[:ASSIGNED_TO_DELIVER]->(o7);
