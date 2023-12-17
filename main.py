import sys
import networkx as nx
from scipy.spatial.distance import euclidean


def create_distance_matrix(locations):
    n = len(locations)
    distance_matrix = [[0] * n for _ in range(n)]
    for i in range(n):
        for j in range(n):
            if i != j:
                distance_matrix[i][j] = euclidean(locations[i][1], locations[j][1])
    return distance_matrix


def find_shortest_route(locations, distance_matrix):
    graph = nx.Graph()
    for i, (name_i, coords_i) in enumerate(locations):
        for j, (name_j, coords_j) in enumerate(locations):
            if i != j:
                graph.add_edge(name_i, name_j, weight=distance_matrix[i][j])

    tsp_route = nx.approximation.traveling_salesman_problem(graph, cycle=True)
    return tsp_route


def parse_locations(args):
    locations = []
    for arg in args:
        # Split each argument by the comma to separate the name and coordinates
        name, lat, lon = arg.split(',')
        locations.append((name, (float(lat), float(lon))))
    return locations


# The first argument is the script name, so you skip it with [1:]
args = sys.argv[1:]

# Parse the locations from the arguments
guadeloupe_locations = parse_locations(args)

# Create the distance matrix and find the shortest route
distance_matrix = create_distance_matrix(guadeloupe_locations)
route = find_shortest_route(guadeloupe_locations, distance_matrix)

# Print the names of the communes in the order of the route
print("Route to take:")
for commune in route:
    print(commune)
