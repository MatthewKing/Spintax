<Query Kind="Statements">
  <NuGetReference>Spintax</NuGetReference>
  <Namespace>Spintax</Namespace>
</Query>

// Get a single permutation.
var testimonial = Spinner.Spin("This library is {cool|rad|awesome}.");
testimonial.Dump();

// Get all permutations.
var testimonials = Spinner.SpinAll("This library is {cool|rad|awesome}.");
testimonials.Dump();
