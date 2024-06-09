<script lang="ts">
	import CategoryList from '$lib/categories/CategoryList.svelte';
	import { getCategories } from '$lib/categories/queries';
	import { ProgressBar } from '@skeletonlabs/skeleton';

	const categoryQuery = getCategories();
</script>

<h2 class="h2">Categories</h2>
{#if $categoryQuery.isLoading}
	<ProgressBar />
{:else if $categoryQuery.data}
	<CategoryList />
{/if}
